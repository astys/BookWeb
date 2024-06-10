import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import backgroundImage from './tea-time.jpg';
import { Link } from 'react-router-dom';
import ReactStars from "react-rating-stars-component";
import { useNavigate } from 'react-router-dom';
import { createBaseAxiosRequest, getTopBooksEndpoint } from '../constans/api';

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
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 170%; //wysokość na stronie 
`;

const Title = styled.h1`
  font-family: 'EB Garamond', serif;
  font-size: 52px;
  color: white; 
  margin-bottom: 20px; 
`;

const SectionTitle = styled.h2`
  font-family: 'EB Garamond', serif;
  font-size: 42px;
  color: white; 
  margin-bottom: 20px; 
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
`;

const BooksContainer = styled.div`
  display: flex;
  justify-content: space-around;
  flex-wrap: wrap;
`;

const Book = styled(Link)`
  width: 150px;
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

  &:hover {
    transform: scale(1.1);
  }
`;

const StyledImage = styled.img`
  width: 150px;
  height: 150px;
  object-fit: cover;
`;

const getBooks = async () => {
  const axiosRequest = createBaseAxiosRequest();
  const response = await axiosRequest.get(getTopBooksEndpoint);
  return response.data;
}

function TopBooks() {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    getBooks().then(response => {
      setBooks(response.highestRated);
    })
  }, []);

  return (
    <>
      <SectionTitle>Top 5 Highest Rated Books</SectionTitle>
      <BooksContainer>
        {books.map(book => (
          <Book key={book.id} to={`/book-details/${book.id}`}>
            <StyledImage src={book.imageUrl} alt={book.title} />
            <h2>{book.title}</h2>
            <p>{book.author}</p>
            <ReactStars
              count={5}
              value={book.rating}
              size={24}
              activeColor="#000"
              isHalf={true}
              edit={false}
            />
          </Book>
        ))}
      </BooksContainer>
    </>
  );
}

function RecentBooks() {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    getBooks().then(response => {
      setBooks(response.recentlyAdded);
    })
  }, []);

  

  return (
    <>
      <SectionTitle>5 Recently Added Books</SectionTitle>
      <BooksContainer>
        {books.map(book => (
          <Book key={book.id} to={`/book-details/${book.id}`}>
            <StyledImage src={book.imageUrl} alt={book.title} />
            <h2>{book.title}</h2>
            <p>{book.author}</p>
          </Book>
        ))}
      </BooksContainer>
    </>
  );
}

const HomePage = () => {
  const navigate = useNavigate();
  const navigateToBookList = () => {
    navigate('/books');
  }

  return (
    <Background>
      <SearchContainer>
        <Title>Search for your favourite book:</Title>
        <SearchBar onFocus={navigateToBookList} type="search" placeholder="Search..."/>
      </SearchContainer>
      <TopBooks />
      <RecentBooks />
    </Background>
  );
}
  export default HomePage;