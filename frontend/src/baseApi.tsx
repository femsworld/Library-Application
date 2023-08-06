import axios from "axios";

const base_Api = "http://localhost:5292/api/v1";

export const fetchData = async () => {
  try {
    const response = await axios.get(`${base_Api}/books`);
    const booksData = response.data.books; // Extract the "books" property
    return booksData;
  } catch (error) {
    throw error;
  }
};
