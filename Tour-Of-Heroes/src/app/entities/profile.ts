import { Blog } from './blog';
import { Comments } from './comment';

export class Profile {
    id: string;
    userName: string;
    email: string;
    profileImage: string;
    fullName: string;
    registeredOn: Date;
    postLikes: number;
    postDislikes: number;
    comments: Comments[];
    blogs: Blog[];
}
