import { Application } from "./application";

export interface PasswordDetails {
    id: number;
    encryptedPassword: string;
    applicationId: number;
    application: Application;
}