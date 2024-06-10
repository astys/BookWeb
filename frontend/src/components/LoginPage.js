import React, { useState, useContext } from 'react';
import styled from 'styled-components';
import backgroundImage from './tea-time.jpg';
import { Link, useNavigate } from 'react-router-dom';
import { createBaseAxiosRequest, loginEndpoint } from '../constans/api';
import AuthContext from './AuthContext';

const Background = styled.div`
  background-image: url(${backgroundImage});
  background-size: cover;
  background-position: center;
  height: 80vh;
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

const LoginForm = styled.form`
  display: flex;
  flex-direction: column;
  width: 500px;
  padding: 20px;
  border-radius: 10px;
  background-color: #f5f5f5;
  font-family: 'EB Garamond', serif;
  align-items: center;
`;

const RegisterLink = styled(Link)`
  margin-top: 10px;
  font-size: 26px;
  color: #000;
  font-family: 'EB Garamond', serif;
`;

const SignUpText = styled.p`
  font-size: 26px;
`;

// Funkcja symulująca użytkownika 
const login = async (email, password) => {
    const axiosRequest = createBaseAxiosRequest();
    const response = await axiosRequest.post(loginEndpoint, {
      email: email,
      password: password
    });

    return response.data.accessToken;
}

const LoginPage = () => {
    const navigate = useNavigate();
    const { setAccessToken, setIsLoggedIn } = useContext(AuthContext);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const accessToken = await login(email, password);
            setAccessToken(accessToken);
            setIsLoggedIn(true);
            navigate('/');
        } catch (err) {
            setError('Invalid login credentials');
        }
    }

    return (
      <Background>
        <LoginForm onSubmit={handleSubmit}>
          <Input type="text" placeholder="E-mail address" value={email} onChange={(e) => setEmail(e.target.value)} />
          <Input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
          <Button type="submit" disabled={!email || !password}>Login</Button>
          {error && <p>{error}</p>}
          <SignUpText>Don't have a profile? Sign up by clicking <RegisterLink to="/register">here</RegisterLink></SignUpText>
        </LoginForm>
      </Background>
    );
}

export default LoginPage;