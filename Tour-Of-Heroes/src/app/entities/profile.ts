import { LikedMovie } from './likedMovie';
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
    jobTitle: string;
    likedMovies: LikedMovie[];
    comments: Comments[];
    blogs: Blog[];
}
