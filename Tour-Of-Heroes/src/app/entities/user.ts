import { Blog } from './blog';
import { Comments } from './comment';

export class User {
    id: string;
    userName: string;
    email: string;
    profileImage: string;
    fullName: string;
    registeredOn: Date;
    comments: Comments[];
    blogs: Blog[];
}
