import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import AuthContext from './AuthContext';
import { createBaseAxiosRequest, logoutEndpoint } from '../constans/api';

const StyledHeader = styled.header`
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: #f5f5f5;
    padding: 20px;
`;

const Logo = styled(Link)`
    font-family: 'Playfair Display', serif;
    font-weight: bold;
    font-size: 65px;
    text-decoration: none;
    color: #000;
    transition: color 1s;

    &:hover {
        color: blue;
    }
`;

const Button = styled(Link)`
    font-family: 'EB Garamond', serif;
    font-size: 28px;
    text-decoration: none;
    color: #000;
    border-radius: 15px;
    border: 2px solid #000;
    padding: 10px;
    background-color: transparent;
    margin-left: 15px;

    &:hover {
        background-color: #000;
        color: #f5f5f5;
        box-shadow: none;
    }
`;

const logout = async (accessToken) => {
    const axiosRequest = createBaseAxiosRequest();
    const response = await axiosRequest.post(logoutEndpoint, null, {
        headers: { Authorization: `Bearer ${accessToken}` }
    });

    return response;
}

const Header = () => {
    const { isLoggedIn, setIsLoggedIn, accessToken, setAccessToken } = useContext(AuthContext);
    const handleLogout = async () => {
        await logout(accessToken);
        setAccessToken(null);
        setIsLoggedIn(false);
    }
    return (
        <StyledHeader>
            <Logo to="/">BookWeb</Logo>
            <div>
                <Button to="/books">Book List</Button>
                {isLoggedIn ? (
                <>
                    <Button to="/my-profile">My Profile</Button>
                    <Button  to="/books/mine">My Bookshelf</Button>
                    <Button onClick={handleLogout}>Log Out</Button>
                </>
                ) : (
                <>
                    <Button  to="/login">Login</Button >
                    <Button  to="/register">Sign Up</Button>
                </>
                )}
            </div>
        </StyledHeader>
        );
}

export default Header;