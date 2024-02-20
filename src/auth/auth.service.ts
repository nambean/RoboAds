import { Injectable } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
const bcrypt = require('bcrypt');

@Injectable()
export class AuthService {
    constructor(private readonly jwtService: JwtService) {
    }

    generateJWT(userInfo: any): Promise<any> {
        const user = {
            id: userInfo.id,
            userName: userInfo.userName,
            fullName: userInfo.fullName,
            team: userInfo.team,
        }

        return this.jwtService.signAsync({ user }, { expiresIn: process.env.SESSION_EXPIRES_IN });
    }

    hashPassword(password: string): Promise<string> {
        return bcrypt.hash(password, 12);
    }

    comparePasswords(newPassword: string, passwordHash: string): Promise<any> {
        return bcrypt.compare(newPassword, passwordHash);
    }
}
