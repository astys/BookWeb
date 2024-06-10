import React, { useEffect, useState, useContext } from 'react';
import styled from 'styled-components';
import backgroundImage from './tea-time.jpg';
import { Link, useParams } from 'react-router-dom';
import ReactStars from "react-rating-stars-component";
import AuthContext from './AuthContext';
import { FaHeart, FaRegHeart } from 'react-icons/fa';
import { createBaseAxiosRequest, getAllBooksEndpoint, getMineBooksEndpoint as getMyBooksEndpoint } from '../constans/api';

const Background = styled.div`
  background-image: url(${backgroundImage});
  background-size: cover;
  background-position: center;
  height: auto;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
`;

const SearchContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`;

const SearchBar = styled.input`
  font-family: 'EB Garamond', serif;
  font-size: 26px;
  width: 600px;
  height: 50px;
  border-radius: 15px; 
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
  border: 2px solid #000;
  padding: 5px 10px; 
  margin-top: 30px;
`;

const Button = styled(Link)`
font-family: 'EB Garamond', serif;
  font-size: 36px;
  text-decoration: none;
  border-radius: 15px; 
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
  border: 2px solid #000;
  padding: 5px 10px; 
  margin-top: 30px;
  margin-left: 20px;
  background-color: #fff;
  color: #000;
  cursor: pointer;

  &:hover {
    transform: scale(1.1);
}
`;

const BooksContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  margin: 20px;
  padding: 20px;
`;

const StyledImage = styled.img`
  width: 150px;
  height: 150px;
  object-fit: cover;
`;

const Book = styled(Link)`
  display: flex;
  width: 800px;
  margin: 10px;
  text-align: center;
  border-radius: 15px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
  padding: 10px;
  background-color: #f5f5f5; 
  font-family: 'EB Garamond', serif;
  color: black;
  text-decoration: none;
  transition: transform 0.3s;
  position: relative;
  margin-top: 20px;

  &:hover {
    transform: scale(1.1);
  }

  img {
      margin-right: 10px;
      margin-left: 10px
    }

  div {
    display: flex;
    flex-direction: column;
    text-align: left;
    h2 {
      margin-bottom: 10px; 
    }
    p {
      margin-bottom: 5px;
    }
  }
`;

const HeartIcon = styled.div`
  position: absolute;
  top: 10px;
  right: 10px;
  font-size: 24px;
  cursor: pointer;
`;

const PaginationNav = styled.nav`
  display: flex;
  justify-content: center;
  margin-top: 20px;
`;

const PageItem = styled.li`
  list-style: none;
  margin: 0 5px;
  display: inline-block;
`;

const PageLink = styled.a`
  padding: 10px;
  border: 1px solid #000;
  color: #000;
  text-decoration: none;

  &:hover {
    background-color: #000;
    color: #fff;
  }
`;

const getBooks = async (pageNumber, pageSize, title) => {
  const axiosRequest = createBaseAxiosRequest();
  const response = await axiosRequest.get(getAllBooksEndpoint, {
    params: {
      pageNumber: pageNumber,
      pageSize: pageSize,
      title: title ?? ''
    }
  });

  return response.data;
}

const getMyBooks = async (pageNumber, pageSize, title, accessToken) => {
  const axiosRequest = createBaseAxiosRequest();
  const response = await axiosRequest.get(getMyBooksEndpoint, {
    params: {
      pageNumber: pageNumber,
      pageSize: pageSize,
      title: title ?? ''
    }, headers: {
      Authorization: `Bearer ${accessToken}`
    }
  });

  return response.data;
}

const Pagination = ({ booksPerPage, totalBooks, paginate }) => {
  const pageNumbers = [];

  for (let i = 1; i <= Math.ceil(totalBooks / booksPerPage); i++) {
      pageNumbers.push(i);
  }

  return (
      <PaginationNav>
          <ul className='pagination'>
              {pageNumbers.map(number => (
                  <PageItem key={number} className='page-item'>
                      <PageLink 
                          onClick={(e) => {
                              e.preventDefault();
                              paginate(number);
                          }} 
                          href='!#' 
                          className='page-link'
                      >
                          {number}
                      </PageLink>
                  </PageItem>
              ))}
          </ul>
      </PaginationNav>
  );
};

//Symulowane dane wszystkich książek
const fetchBooks = async (pageNumber, pageSize, title, accessToken) => {
    if (accessToken) {
      const response = getMyBooks(pageNumber, pageSize, title, accessToken);
      return response;
    } else {
      const response = await getBooks(pageNumber, pageSize, title);
      return response;
    }
  }

  function BookListPage() {
    const { id } = useParams();
    const { accessToken } = useContext(AuthContext);
    const [books, setBooks] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalBooks, setTotalBooks] = useState(0);
    const booksPerPage = 5;
    const { isLoggedIn } = useContext(AuthContext);
    const [title, setTitle] = useState('');

    useEffect(() => {
      if (id && accessToken) {
        fetchBooks(currentPage, booksPerPage, title, accessToken).then(x => {
          setBooks(x.books);
          setTotalBooks(x.totalBooks);
        }).catch(err => {
          setBooks([]);
        });
      } else {
        fetchBooks(currentPage, booksPerPage, title).then(x => {
          setBooks(x.books);
          setTotalBooks(x.totalBooks);
        }).catch(err => {
          setBooks([]);
        });
      }
        
    }, [currentPage]);

    useEffect(() => {
      setCurrentPage(1);
      if (id && accessToken) {
        fetchBooks(currentPage, booksPerPage, title, accessToken).then(x => {
          setBooks(x.books);
          setTotalBooks(x.totalBooks);
        }).catch(err => {
          setBooks([]);
        });
      } else {
        fetchBooks(currentPage, booksPerPage, title).then(x => {
          setBooks(x.books);
          setTotalBooks(x.totalBooks);
        }).catch(err => {
          setBooks([]);
        });
      }
    }, [title, id]);

    // Zmiana strony
    const paginate = pageNumber => setCurrentPage(pageNumber);

    return (<Background>
      <SearchContainer>
          <SearchBar type="search" placeholder="Search for a book..." onChange={(e) => setTitle(e.target.value)}/>
          {isLoggedIn && <Button to="/add-book">Add a book</Button>}
        </SearchContainer>
        <BooksContainer>
            {books.map(book => (
                <Book key={book.id} to={`/book-details/${book.id}`}>
                   {isLoggedIn && <HeartIcon onClick={() => {/* call add to library function here */}}>
                        {book.inLibrary ? <FaHeart /> : <FaRegHeart />}
                    </HeartIcon>}
                <ReactStars 
                    count={5}
                    value={book.rating}
                    size={24}
                    activeColor="#000"
                    isHalf={true}
                    edit={false}
                />
                <StyledImage src={book.imageUrl} alt={book.title} />
                <div>
                  <h2>{book.title}</h2>
                  <p>{book.author}</p>
                  <p>{book.genre}</p>
                  <p>{book.description.substring(0, 150)}...</p>
                </div>
            </Book>
            ))}
            <Pagination 
                booksPerPage={booksPerPage} 
                totalBooks={totalBooks} 
                paginate={paginate} 
            />
        </BooksContainer>
        </Background>
    )
}

export default BookListPage;