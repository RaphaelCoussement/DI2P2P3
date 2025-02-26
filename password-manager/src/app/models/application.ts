export interface Application {
    id: number;
    name: string;
    type: ApplicationType;
}

export enum ApplicationType {
    GrandPublic,
    Professionnelle
}