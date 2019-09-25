import { EditHistories } from './editHistories';

export class Hero {
    id: number;
    name: string;
    description: string;
    image: File;
    coverImage: File;
    realName: string;
    birthday: Date;
    gender: string;
    editHistories: EditHistories;
}
