import { Comments } from './comment';
import { EditHistory } from './editHistory';
import Movie from './movie';

export class Hero {
    id: number;
    name: string;
    description: string;
    image: File;
    coverImage: File;
    realName: string;
    birthday: Date;
    gender: string;
    editHistory: EditHistory[];
    movies: Movie[];
    comments: Comments[];
}
