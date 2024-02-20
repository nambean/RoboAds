import { CanActivate, ExecutionContext, Injectable } from '@nestjs/common';
import { Observable } from 'rxjs';
import { UserService } from '../../user/user.service';
import { UserEntity } from '../../user/user.entity';
import { map } from 'rxjs/operators';

@Injectable()
export class RolePermissionGuard implements CanActivate {
    constructor(
        private userService: UserService
    ) {
    }

    canActivate(context: ExecutionContext): boolean | Promise<boolean> | Observable<boolean> {
        const request = context.switchToHttp().getRequest();
        const user: UserEntity = request.user;

        return this.userService.getUserRoleAndPermission(user.id, request.route.path).pipe(
            map((permission: any) => !!permission));
    }
}
