import React, { useEffect, useState } from 'react';
import styled from 'styled-components';
import backgroundImage from './tea-time.jpg';
import { useNavigate } from 'react-router-dom';
import { createBaseAxiosRequest, registerEndpoint, getAllGenresEndpoint } from '../constans/api';

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

const SignUpForm = styled.form`
  display: flex;
  flex-direction: column;
  width: 500px;
  padding: 20px;
  border-radius: 10px;
  background-color: #f5f5f5;
  font-family: 'EB Garamond', serif;
  align-items: center;
`;


const Checkbox = styled.input.attrs({ type: 'checkbox' })`
  margin-right: 10px;
`;

const Label = styled.label`
font-size: 26px;
  display: flex;
  align-items: center;
  margin-bottom: 20px;
`;

const register = async (email, password, userName, genres) => {
  const axiosRequest = createBaseAxiosRequest();
  const response = await axiosRequest.post(registerEndpoint, {
    email: email,
    password: password,
    name: userName,
    genres: genres ?? []
  });
}

const getAllGenres = async () => {
  const axiosRequest = createBaseAxiosRequest();
  const response = await axiosRequest.get(getAllGenresEndpoint);
  return response.data.genres;
}

const RegisterPage = () => {
    const navigate = useNavigate();
    const [bookGenres, setBookGenres] = useState([]);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [userName, setUserName] = useState('');
    const [reenteredPassword, setReenteredPassword] = useState('');

    //Funkcja symulująca dane z backendu
    useEffect(() => {
      getAllGenres().then((genres) => {
        genres.forEach(element => {
          element.selected = false;
        });
        setBookGenres(genres);
      });
      }, []);

    const handleSubmit = async (event) => {
      event.preventDefault();
      try {
        const selectedGenreIds = bookGenres.filter(x => x.selected).map(x => x.id);
        await register(email, password, userName, selectedGenreIds);
        alert('Registered successfully, you can now log in.');
        navigate('/login');
      } catch (err) {
        alert("Error occurred during registration.\n" + err);
      }
    }

    const checkGenre = (index) => {
      bookGenres[index].selected = !bookGenres[index].selected;
    }

    return (
      <Background>
        <SignUpForm onSubmit={handleSubmit}>
        <Input placeholder="E-mail address" value={email} onChange={(e) => setEmail(e.target.value)} />
        <Input placeholder="Name" value={userName} onChange={(e) => setUserName(e.target.value)} />
        <Input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
        <Input type="password" placeholder="Reenter password" value={reenteredPassword} onChange={(e) => setReenteredPassword(e.target.value)} />

        <Label>Choose your favourite genres: </Label>
        {bookGenres.map((genre, index) => (
          <Label key={genre.id}>
            <Checkbox onChange={() => checkGenre(index)} /> {genre.name}
          </Label>
        ))}

        <Button type="submit" disabled={password !== reenteredPassword || !userName || !password || !reenteredPassword}>Sign Up</Button>
      </SignUpForm>
      </Background>
    );
  }
    export default RegisterPage;