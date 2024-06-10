import React, { useContext, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import styled from 'styled-components';
import backgroundImage from './tea-time.jpg';
import { createBaseAxiosRequest, getLoggedUserEndpoint } from '../constans/api';
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

const MyProfileForm = styled.form`
    display: flex;
    flex-direction: column;
    width: 500px;
    padding: 20px;
    border-radius: 10px;
    background-color: #f5f5f5;
    font-family: 'EB Garamond', serif;
    align-items: flex-start;
`;

const Title = styled.h1`
    text-align: center;
    width: 100%;
    font-size: 36px;
`;

const UserInfo = styled.p`
    font-size: 20px;
`;

const getUser = async (accessToken) => {
    const axiosRequest = createBaseAxiosRequest();
    const response = await axiosRequest.get(getLoggedUserEndpoint, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response.data;
} 

const MyProfilePage = () => {
    const [user, setUser] = useState({});
    const { accessToken } = useContext(AuthContext);

    // Symulacja pobierania danych z backendu
    useEffect(() => {
        getUser(accessToken).then(x => {
            setUser(x);
        })
    }, []);

    return (
        <Background>
            <MyProfileForm>
                <Title>My Profile</Title>
                <UserInfo>
                    <p>E-mail address: {user.emailAddress}</p>
                    <p>Name: {user.name}</p>
                    <p>Favourite Genres: {user.favouriteGenres && user.favouriteGenres.join(', ')}</p>
                    <p>Number of Reviews Posted: {user.reviewsCount}</p>
                </UserInfo>
            </MyProfileForm>
        </Background>
    );
}
    export default MyProfilePage;