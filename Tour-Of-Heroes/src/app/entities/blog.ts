import { Comments } from './comment';
export class Blog {
    id: number;
    authorUserName: string;
    title: string;
    blogImage: string;
    publishedOn: Date;
    content: string;
    likes: number;
    comments: Comments[];
}
