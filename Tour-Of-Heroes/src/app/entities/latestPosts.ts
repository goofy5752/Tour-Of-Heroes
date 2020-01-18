import { Comments } from './comment';

export class LatestPosts {
    id: number;
    authorUserName: string;
    title: string;
    blogImage: string;
    publishedOn: Date;
    comments: Comments[];
}
