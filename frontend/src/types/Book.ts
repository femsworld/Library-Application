export interface Book{
    id: string;
    title: string;
    genre: Genre;
    images?: string[] | null;
}

export enum Genre {
    TextBooks,
    Novel,
    Fiction,
    ResearchPaper
  }