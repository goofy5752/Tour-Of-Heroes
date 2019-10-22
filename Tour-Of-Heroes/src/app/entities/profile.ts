import { Comments } from './comment';

export class Profile {
    id: string;
    username: string;
    email: string;
    fullName: string;
    comments: Comments[];
}
