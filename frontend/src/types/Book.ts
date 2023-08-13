export interface Book{
    title: string;
    genre: Genre;
    images?: string[] | null;
}

enum Genre {
    TextBooks,
    Novel,
    Fiction,
    ResearchPaper
  }