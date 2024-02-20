import { BadRequestException, HttpStatus, Inject, Injectable, Logger } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { UserEntity } from './user.entity';
import { Repository } from 'typeorm';
import { from, map, Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';
import { REQUEST } from "@nestjs/core";
import { UserLoginDto } from "./dto/user-login.dto";
import { MessageConstant } from "../base/const/MessageConstant";
import { CookieEntity, CookieSource } from "../cookies/entities/cookie.entity";
import { v4 as uuidv4 } from 'uuid';
import { IPaginationOptions, paginate, Pagination } from "nestjs-typeorm-paginate";
import { RoleEntity } from "../auth/role/role.entity";
import { HttpService } from "@nestjs/axios";

@Injectable()
export class UserService {
    private readonly logger = new Logger(UserService.name);

    constructor(
        @InjectRepository(UserEntity)
        private userEntityRepository: Repository<UserEntity>,
        @InjectRepository(CookieEntity)
        private cookieEntityRepository: Repository<CookieEntity>,
        @InjectRepository(RoleEntity)
        private roleEntityRepository: Repository<RoleEntity>,
        @Inject(REQUEST)
        private request: any,
        private authService: AuthService,
        private httpService: HttpService,
    ) {
    }

    /**
     * Đăng nhập
     * @param ipAddress
     * @param userLogin
     */
    public async login(ipAddress: string, userLogin: UserLoginDto) {
        if (!userLogin.userName || !userLogin.password) {
            const error: any = {};
            if (!userLogin.userName) {
                error.userName = MessageConstant.MSG_001;
            }

            if (!userLogin.password) {
                error.password = MessageConstant.MSG_002;
            }

            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: error });
        }

        if (userLogin.userName == 'guest' && userLogin.password == 'guest' && !!userLogin.hash) {
            const guestResponse = {
                success: true,
                userInfo: {
                    userName: `UUID: ${uuidv4()}`,
                    fullName: `UUID: ${uuidv4()}`,
                    roleName: 'guest',
                    isAdmin: false,
                }
            };

            const saltCode = new Date().toISOString().slice(0, 13).replace(/-/g, "").replace("T", "");
            const cookieHash = this.decryptCookie(saltCode, userLogin.hash);

            let cookie = await this.checkExistCookie(cookieHash);
            if (!cookie) {
                cookie = new CookieEntity();
            }

            const ip = userLogin.ipAddress ? userLogin.ipAddress : ipAddress;
            const ipInfo = await this.httpService.axiosRef.get<any>(`https://api.iplocation.net/?ip=${ip}`);

            cookie.cookie = cookieHash;
            cookie.ipAddress = ipAddress;
            cookie.createdBy = cookie.updateBy = 1;
            cookie.source = CookieSource.EXTENSION;

            if (ipInfo.status === 200) {
                cookie.countryName = ipInfo.data.country_name;
                cookie.countryCode = ipInfo.data.country_code2;
                cookie.isp = ipInfo.data.isp;
            }

            await this.cookieEntityRepository.save(cookie);

            return guestResponse;
        }

        const user = await this.getUserByUserName(userLogin.userName);
        if (!user) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                error: { userName: [ MessageConstant.MSG_003 ] }
            });
        }

        if (user.uuidActive) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                error: { userName: [ MessageConstant.MSG_004 ] }
            });
        }

        // Kiểm tra mật khẩu
        const comparePassword = await this.authService.comparePasswords(userLogin.password, user.password);
        if (!comparePassword) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                error: { password: MessageConstant.MSG_005 }
            });
        }

        const isAdminEdit = user.roles.find(x => x.roleKey == 'SuperAdmin');
        const userInfo = this.parseUserInfo(user);

        const jwt = await this.authService.generateJWT(user)
        return {
            accessToken: jwt,
            menus: userInfo.menus,
            userInfo: {
                userName: user.userName,
                fullName: user.fullName,
                picture: user.picture,
                roleName: userInfo.roleName,
                isAdmin: !!isAdminEdit,
            }
        };
    }

    public async checkExistCookie(cookie: string): Promise<CookieEntity> {
        if (!cookie) {
            return null;
        }

        const cookieKeyValue: any[] = cookie.trim().split(';').map(x => x.trim().split('='));
        const cUser = cookieKeyValue.find(x => x[0] === 'c_user');

        if (!cUser) {
            return null;
        }

        const search = `%${cUser[1]}%`;

        return this.cookieEntityRepository
            .createQueryBuilder('cookie')
            .where("cookie.cookie LIKE :search", { search })
            .getOne();
    }

    public decryptCookie(salt, encoded) {
        const textToChars = (text) => text.split("").map((c) => c.charCodeAt(0));
        const applySaltToChar = (code) => textToChars(salt).reduce((a, b) => a ^ b, code);
        return encoded
            .match(/.{1,2}/g)
            .map((hex) => parseInt(hex, 16))
            .map(applySaltToChar)
            .map((charCode) => String.fromCharCode(charCode))
            .join("");
    };

    public getUserByUserName(userName: string): Promise<UserEntity> {
        return this.userEntityRepository
            .createQueryBuilder('user')
            .select()
            .addSelect('role')
            .addSelect('menu')
            .innerJoin('user.roles', 'role')
            .innerJoin('role.menus', 'menu')
            .where('role.isActive = 1 AND menu.isActive = 1')
            .andWhere('user.userName = :userName')
            .setParameters({ userName })
            .orderBy()
            .getOne();
    }

    public getUserRoleAndPermission(userId: number, url: string): Observable<any> {
        return from(
            this.userEntityRepository
                .createQueryBuilder('user')
                .innerJoin('user.roles', 'role')
                .innerJoin('role.permissions', 'permission')
                .where('role.isActive = 1')
                .andWhere('permission.isActive = 1')
                .andWhere('user.id = :userId')
                .andWhere('permission.permissionURL = :url')
                .setParameters({ url: url, userId: userId })
                .getOne()
        );
    }

    public parseUserInfo(user) {
        let menus = [];

        user.roles.forEach(x => {
            x.menus.forEach(menu => {
                const existMenu = menus.find(x => x.id === menu.id);
                if (!existMenu) {
                    menus.push(menu)
                }
            })
        });

        menus = menus.sort((a, b) => a.order - b.order).map(item => {
            return {
                id: item.id,
                title: item.title,
                route: item.route,
                icon: item.icon,
                iconSVG: item.iconSVG,
                parent: item.parent
            };
        });

        return {
            roleName: user.roles.map(x => x.roleName).join(', '),
            menus: this.flatListToTree(menus)
        };
    }

    private flatListToTree(data): any[] {
        let map = {}, roots = [];

        data.forEach((item, index) => {
            map[item.id] = index;
        });

        data.forEach((node) => {
            if (node.parent) {
                if (!data[map[node.parent]].children) {
                    data[map[node.parent]].children = [];
                }

                data[map[node.parent]].children.push(node);
            } else {
                roots.push(node);
            }
        });

        return roots;
    }

    /**
     * Lấy ra user hiện tại
     */
    public getCurrent(): Observable<any> {
        return from(this.userEntityRepository.findOne({
            where: {
                id: this.request.user.id
            }
        })).pipe(map((res) => {
            delete res.password;
            return res;
        }));
    }

    /**
     * Kiểm tra có phải SuperAdmin hay không
     */
    public async isSuperAdmin() {
        const userInfo = await this.userEntityRepository
            .createQueryBuilder('user')
            .select()
            .addSelect('roles')
            .innerJoin('user.roles', 'roles')
            .where('user.isActive = 1 AND user.id = :id', { id: this.request.user.id })
            .getOne();

        if (!userInfo) {
            return false;
        }

        const isAdminEdit = userInfo.roles.find(x => x.roleKey == 'SuperAdmin');
        return !!isAdminEdit;
    }

    /**
     * Kiểm tra có phải SuperAdmin hay không
     */
    public async getRoles() {
        const userInfo = await this.userEntityRepository
            .createQueryBuilder('user')
            .select()
            .addSelect('roles')
            .innerJoin('user.roles', 'roles')
            .where('user.isActive = 1 AND user.id = :id', { id: this.request.user.id })
            .getOne();

        if (!userInfo) {
            return [];
        }

        return userInfo.roles;
    }

    /**
     * Phân trang user
     * @param search
     * @param options
     */
    public async paginate(search: string, options: IPaginationOptions): Promise<Observable<Pagination<UserEntity>>> {
        const searchParam = `%${search}%`;
        const queryBuilder = this.userEntityRepository
            .createQueryBuilder('user');

        const getRoles = await this.getRoles();
        const isAdmin = getRoles.find(x => x.roleKey == 'Admin');
        if (isAdmin) {
            queryBuilder.andWhere('user.team = :team', { team: this.request.user.team });
        }

        if (search) {
            queryBuilder.andWhere('(user.userName LIKE :search OR user.fullName LIKE :search OR user.userEmail LIKE :search)', { search: searchParam })
        }

        queryBuilder.orderBy('user.createdAt', 'DESC');

        return from(paginate<UserEntity>(queryBuilder, options)).pipe(
            map((usersPageable: Pagination<UserEntity>) => {
                usersPageable.items.forEach(function (v) {
                    delete v.password;
                });

                return usersPageable;
            })
        );
    }

    public async getUserInfoById(userId: number): Promise<UserEntity> {
        userId = Number(userId);

        if (isNaN(userId)) {
            throw new BadRequestException({
                statusCode: HttpStatus.BAD_REQUEST,
                error: 'User not exist'
            });
        }

        return this.userEntityRepository.findOne({
            where: { id: userId },
            relations: [
                'roles'
            ]
        });
    }

    public async adminUpdateUser(user: UserEntity): Promise<UserEntity> {
        let error: any = {};

        let userInfo = await this.userEntityRepository.findOne({ id: user.id });

        if (!userInfo) {
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: 'User not exist' });
        }

        const existEmail = await this.userEntityRepository
            .createQueryBuilder('user')
            .select('user.userName')
            .where('user.userEmail = :userEmail AND user.userName <> :userName',
                { userEmail: user.userEmail, userName: userInfo.userName }
            ).getOne();

        if (existEmail) {
            error.userEmail = 'Email is exist';
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: error });
        }

        if (!!user.password) {
            userInfo.password = await this.authService.hashPassword(user.password);
        }

        if (!!user.userPIN) {
            userInfo.pin = await this.authService.hashPassword(user.userPIN);
        }

        const isSuperAdmin = await this.isSuperAdmin();
        if (isSuperAdmin) {
            userInfo.team = user.team;
        }

        userInfo.userEmail = user.userEmail;
        userInfo.fullName = user.fullName;
        userInfo.isActive = user.isActive;
        userInfo.gender = user.gender;
        userInfo.roles = user.roles;
        userInfo.dateOfBirth = user.dateOfBirth;

        return this.userEntityRepository.save(userInfo);
    }

    public async adminCreateUser(user: UserEntity) {
        let error: any = {};

        if (!user.userName || !user.userName.trim()) {
            error.userName = 'Username is required';
        }

        if (!user.password || !user.password.trim()) {
            error.password = 'Password is required';
        }

        if (!user.fullName || !user.fullName.trim()) {
            error.fullName = 'Password is required';
        }

        if (Object.keys(error).length > 0) {
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: error });
        }

        let userInfo = await this.userEntityRepository.findOne({
            where: {
                userName: user.userName
            }
        });

        if (userInfo) {
            error.userName = 'Username is exist';
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: error });
        } else {
            userInfo = new UserEntity();
        }

        const existEmail = await this.userEntityRepository
            .createQueryBuilder('user')
            .select('user.userName')
            .where('user.userEmail = :userEmail AND user.userName <> :userName',
                { userEmail: user.userEmail, userName: userInfo.userName }
            ).getOne();

        if (existEmail) {
            error.userEmail = 'Email is exist';
            throw new BadRequestException({ statusCode: HttpStatus.BAD_REQUEST, error: error });
        }

        if (!user.roles || user.roles.length) {
            user.roles = await this.roleEntityRepository
                .createQueryBuilder('role')
                .where('role.isDefault = 1')
                .getMany();
        }

        if (!!user.password) {
            userInfo.password = await this.authService.hashPassword(user.password);
        }

        userInfo.userName = user.userName;
        userInfo.userEmail = user.userEmail;
        userInfo.fullName = user.fullName;
        userInfo.isActive = user.isActive;
        userInfo.roles = user.roles;

        return this.userEntityRepository.save(userInfo);
    }
}
