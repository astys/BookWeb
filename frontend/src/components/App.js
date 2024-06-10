import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './HomePage';
import Header from './Header';
import BookListPage from './BookListPage';
import LoginPage from './LoginPage';
import RegisterPage from './RegisterPage';
import AddBookPage from './AddBookPage';
import MyProfilePage from './MyProfilePage';
import BookDetailPage from './BookDetailPage';
import AuthContext from './AuthContext';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(!!localStorage.getItem('accessToken'));
  const [accessToken, setAccessToken] = useState(localStorage.getItem('accessToken'));

  useEffect(() => {
    if (accessToken) {
      localStorage.setItem('accessToken', accessToken);
      setIsLoggedIn(true);
    } else {
      localStorage.removeItem('accessToken');
      setIsLoggedIn(false);
    }
  }, [accessToken]);

  return (
    <AuthContext.Provider value={
      { isLoggedIn, setIsLoggedIn, accessToken, setAccessToken }}>
      <Router>
        <div>
          <Header />
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/books" element={<BookListPage />} />
            <Route path="/books/:id" element={<BookListPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/add-book" element={<AddBookPage />} />
            <Route path="/book-details/:id" element={<BookDetailPage />} />
            <Route path="/my-profile" element={<MyProfilePage />} />
          </Routes>
        </div>
      </Router>
    </AuthContext.Provider>
  );
}

export default App;