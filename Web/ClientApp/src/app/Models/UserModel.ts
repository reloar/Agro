export class UserModel {
    rememberMe: boolean;
    email: string;
    verificationCode: string;
    grantType = 'password';
    username: string;
    password = '';
    userId: string;
    role: string;
    fullName: string;
    bankSortCode: string;
    walletCodec: string;
    contactAddress: string;
    bankAccount: string;
    subAccountName: string;

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
