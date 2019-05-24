export class UserModel {
    rememberMe: boolean;
    email: string;
    verificationCode: string;
    grantType = 'password';
    username = this.email;
    password = '';
    }
export class UserLogin {
        id: string;
        email: string;
        password: string;
        firstName: string;
        lastName: string;
        token?: string;
        roles: string;
      }
export class IdToken {
        sub: string;
        name: string;
        fullname: string;
        email: string;
        role: string | string[];
        configuration: string;
      }
