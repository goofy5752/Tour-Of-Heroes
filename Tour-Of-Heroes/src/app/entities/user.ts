import { Blog } from './blog';
import { Comments } from './comment';
import { UserActivity } from './userActivity';

export class User {
    id: string;
    userName: string;
    email: string;
    profileImage: string;
    fullName: string;
    registeredOn: Date;
    role: string;
    comments: Comments[];
    blogs: Blog[];
    activity: UserActivity[];
}
