import { LatestPosts } from './latestPosts';
import { Comments } from './comment';

export class Blog {
    id: number;
    authorUserName: string;
    title: string;
    blogImage: string;
    publishedOn: Date;
    content: string;
    likes: number;
    dislikes: number;
    currentUser: string;
    comments: Comments[];
    latestPosts: LatestPosts[];
}
