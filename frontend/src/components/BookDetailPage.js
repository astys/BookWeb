import React, { useEffect, useState, useContext } from 'react';
import { useParams } from 'react-router-dom';
import styled from 'styled-components';
import backgroundImage from './tea-time.jpg';
import AuthContext from './AuthContext'
import ReactStars from "react-rating-stars-component";
import { addReviewEndpoint, createBaseAxiosRequest, getBookByIdEndpoint } from '../constans/api';

const Background = styled.div`
    background-image: url(${backgroundImage});
    background-size: cover;
    background-position: center;
    height: auto;
    min-height: 80vh;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    padding-top: 100px;
`;

const BookDetailForm = styled.form`
    display: flex;
    flex-direction: column;
    width: 700px;
    padding: 20px;
    border-radius: 10px;
    background-color: #f5f5f5;
    font-family: 'EB Garamond', serif;
    align-items: flex-start;
    h2 {
        font-size: 36px;
    }
    p {
        font-size: 20px;
    }
`;

const ReviewContainer = styled.div`
    margin-top: 20px;
    border-top: 1px solid #ccc;
    padding-top: 20px;
`;

const ReviewBox = styled.div`
    border: 1px solid #ccc;
    border-radius: 10px;
    background-color: #f5f5f5;
    padding: 10px;
    margin-bottom: 10px;
    font-family: 'EB Garamond', serif;
    width: 650px;
`;

const Review = ({ review }) => (
    <ReviewBox>
        <h4>{review.name}</h4>
        <p>{new Date(review.date).toLocaleDateString()}</p>
        <ReactStars count={5} value={review.rating} size={24} activeColor="#000" isHalf={true} edit={false} />
        <p>{review.comment}</p>
    </ReviewBox>
);

const AddReviewForm = styled.form`
    display: flex;
    flex-direction: column;
    width: 700px;
    padding: 20px;
    border-radius: 10px;
    background-color: #f5f5f5;
    font-family: 'EB Garamond', serif;
    align-items: flex-start;
    margin-top: 20px;
`;

const Button = styled.button`
    margin-top: 20px;
    padding: 10px;
    width: 200px;
    font-size: 16px;
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
`;

const StyledImage = styled.img`
  width: 150px;
  height: 150px;
  object-fit: cover;
`;

const NewCommentInput = styled.textarea`
    margin-bottom: 30px;
    padding: 10px;
    font-size: 20px;
    width: 600px;
    height: 200px;
    border-radius: 15px;
    border: 1px solid #000;
    font-family: 'EB Garamond', serif;
    resize: none;
`;

const getBookById = async (id, accessToken) => {
    const axiosRequest = createBaseAxiosRequest();
    const response = await axiosRequest.get(getBookByIdEndpoint, {
        params: {
            id: id
        },
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });

    return response.data;
}

const addReview = async (bookId, rating, comment, accessToken) => {
    const axiosRequest = createBaseAxiosRequest();
    await axiosRequest.post(addReviewEndpoint, {
        bookId: bookId,
        rating: rating,
        comment: comment
    }, {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    });
}

const BookDetailPage = () => {
    const { id } = useParams();
    const [book, setBook] = useState(null);
    const [reviews, setReviews] = useState([]);
    const [newRating, setNewRating] = useState(0);
    const [newComment, setNewComment] = useState('');
    const [alreadyReviewed, setAlreadyReviewed] = useState(false);
    const { isLoggedIn, accessToken } = useContext(AuthContext);

    const handleAddReview = (event) => {
        event.preventDefault();
        addReview(book.id, newRating, newComment, accessToken).then(x => {
            getBookById(id, accessToken).then(x => {
                setBook(x);
                setReviews(x.reviews);
                setAlreadyReviewed(x.alreadyReviewed);
            });
        });
    };

    useEffect(() => {
        getBookById(id, accessToken).then(x => {
            setBook(x);
            setReviews(x.reviews);
            setAlreadyReviewed(x.alreadyReviewed);
        });
    }
    ,[id]);

    return (
    <Background>
    {book && (
        <>
        <BookDetailForm>
            <StyledImage src={book.imageUrl} alt={book.title} />
            <h2>{book.title}</h2>
            <p>Author: {book.author}</p>
            <p>Genre: {book.genre}</p>
            <ReactStars count={5} value={book.rating} size={24} activeColor="#000" isHalf={true} edit={false} />
            <p>Description: {book.description}</p>
        </BookDetailForm>
        {isLoggedIn && !alreadyReviewed && <AddReviewForm onSubmit={handleAddReview}>
                <NewCommentInput name="comment" placeholder="Comment" required value={newComment} onChange={(e) => setNewComment(e.target.value)}></NewCommentInput>
                <ReactStars count={5} value={newRating} size={24} activeColor="#000" isHalf={true} onChange={setNewRating} />
                <Button type="submit">Add Review</Button>
        </AddReviewForm>}
        <ReviewContainer>
            {reviews.map((review, index) => (
            <Review key={index} review={review} />
            ))}
        </ReviewContainer>
        </>
    )}
    </Background>
);
}
    export default BookDetailPage;