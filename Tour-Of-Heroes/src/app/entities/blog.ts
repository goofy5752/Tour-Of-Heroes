import { Comments } from './comment';
export class Blog {
    id: number;
    userName: string;
    title: string;
    publishedOn: Date;
    content: string;
    comments: Comments[];
}
