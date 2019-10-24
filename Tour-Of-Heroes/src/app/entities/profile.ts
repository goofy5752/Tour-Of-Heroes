import { Comments } from './comment';

export class Profile {
    id: string;
    userName: string;
    email: string;
    profileImage: string;
    fullName: string;
    comments: Comments[];
}
