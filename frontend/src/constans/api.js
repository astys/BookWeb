import axios from "axios";

const baseUrl = "http://localhost:5252/";
const authGroup = "auth";
const genreGroup = "genre";
const user = "user";
const book = "book";
export const loginEndpoint = `${authGroup}/login`;
export const registerEndpoint = `${user}/add`;
export const logoutEndpoint = `${authGroup}/logout`;
export const getAllGenresEndpoint = `${genreGroup}/getAll`;
export const getAllBooksEndpoint = `${book}/getAll`;
export const getBookByIdEndpoint = `${book}/get`;
export const getTopBooksEndpoint = `${book}/getTop`;
export const getMineBooksEndpoint = `${book}/getMine`;
export const addReviewEndpoint = `${book}/addReview`;
export const addBookEndpoint = `${book}/add`;
export const getLoggedUserEndpoint = `${user}/get`;

export const createBaseAxiosRequest = () => {
    const request = axios.create({
        baseURL: baseUrl,
        timeout: 50000,
        headers: {
            'Content-Type': 'application/json'
        },
        withCredentials: false
    });

    return request;
}