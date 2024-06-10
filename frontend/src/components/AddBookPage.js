import React, { useEffect, useState, useContext } from 'react';
import styled from 'styled-components';
import backgroundImage from './tea-time.jpg';
import AuthContext from './AuthContext'
import { addBookEndpoint, createBaseAxiosRequest, getAllGenresEndpoint } from '../constans/api';
import { useNavigate } from 'react-router-dom';

const Background = styled.div`
  background-image: url(${backgroundImage});
  background-size: cover;
  background-position: center;
  height: auto;
  min-height: 80vh;
  display: flex;
  justify-content: flex-start;
  align-items: center;  
  flex-direction: column; 
  padding-top: 100px;
`;

const Input = styled.input`
  margin-bottom: 30px;
  padding: 10px;
  font-size: 26px;
  width: 300px;
  height: 30px;
  border-radius: 15px;
  border: 1px solid #000;
  font-family: 'EB Garamond', serif;
`;

const DescriptionInput = styled.textarea`
  margin-bottom: 30px;
  padding: 10px;
  font-size: 26px;
  width: 400px;
  height: 400px;
  border-radius: 15px;
  border: 1px solid #000;
  font-family: 'EB Garamond', serif;
  resize: none;
`;

const GenreLabel = styled.p`
  font-size: 26px;
  font-weight: bold;
  margin-bottom: 10px;
`;

const Button = styled.button`
  padding: 10px;
  width: 300px;
  font-size: 26px;
  border: 2px solid #000;
  border-radius: 15px;
  background-color: #000;
  color: #f5f5f5;
  font-family: 'EB Garamond', serif;

  &:hover {
    background-color: #f5f5f5;
    color: #000;
    box-shadow: none;
  }

  ${props => props.disabled && `
    background-color: #ccc;
    color: #666;
    border: 2px solid #666;
    cursor: not-allowed;
    pointer-events: none;

    &:hover {
      background-color: #ccc;
      color: #666;
      box-shadow: none;
    }
  `}
`;
const AddBookForm = styled.form`
  display: flex;
  flex-direction: column;
  width: 500px;
  padding: 20px;
  border-radius: 10px;
  background-color: #f5f5f5;
  font-family: 'EB Garamond', serif;
  align-items: center;
`;

const RadioButton = styled.input.attrs({ type: 'radio' })`
  margin-right: 10px;
  
`;

const Label = styled.label`
font-size: 26px;
  display: flex;
  align-items: center;
  margin-bottom: 20px;
`;

const addBook = async (title, author, description, genreId, imageUrl, accessToken) => {
  const axiosRequest = createBaseAxiosRequest();
  await axiosRequest.post(addBookEndpoint, {
    title: title,
    author: author,
    description: description,
    genreId: genreId,
    imageUrl: imageUrl
  }, {
    headers: {
      Authorization: `Bearer ${accessToken}`
    }
  });
}

const getAllGenres = async () => {
  const axiosRequest = createBaseAxiosRequest();
  const response = await axiosRequest.get(getAllGenresEndpoint);
  return response.data.genres;
}

const AddBookPage = () => {
    const [bookGenres, setBookGenres] = useState([]);
    const [selectedGenre, setSelectedGenre] = useState(null);
    const [title, setTitle] = useState('');
    const [author, setAuthor] = useState('');
    const [imageUrl, setImageUrl] = useState('');
    const [description, setDescription] = useState('');
    const { accessToken } = useContext(AuthContext);
    const navigate = useNavigate();

    //Funkcja symulująca dane o gatunkach książek ze słownika z bazy danych
    useEffect(() => {
      getAllGenres().then((genres) => {
        genres.forEach(element => {
          element.selected = false;
        });
        setBookGenres(genres);
        setSelectedGenre(genres[0].id);
      });
      }, []);

    const handleGenreChange = (event) => {
      setSelectedGenre(event.target.value);
    };
  
    const saveBook = (event) => {
      // Function to save the book to the database
      event.preventDefault();
      addBook(title, author, description, selectedGenre, imageUrl, accessToken)
      .then(() => {
        alert('Successfully added book.');
        navigate('/');
      })
    };

    return (
      <Background>
        <AddBookForm onSubmit={saveBook}>
        <Input placeholder="Title" value={title} onChange={(e) => setTitle(e.target.value)} />
        <Input placeholder="Author" value={author} onChange={(e) => setAuthor(e.target.value)}/>
        <Input placeholder="Image url" value={imageUrl} onChange={(e) => setImageUrl(e.target.value)}/>
        <GenreLabel>Genre:</GenreLabel>
        {bookGenres.map((genre, index) => (
          <Label key={genre.id}>
            <RadioButton name="genre"
              value={genre.id}
              checked={selectedGenre === genre.id.toString()}
              onChange={handleGenreChange} /> {genre.name}
          </Label>
        ))}
        <DescriptionInput placeholder="Description" value={description} onChange={(e) => setDescription(e.target.value)} />
        <Button type="submit" disabled={!title || !author || !selectedGenre}>
          Save
          </Button>
      </AddBookForm>
      </Background>
    );
  }
  export default AddBookPage