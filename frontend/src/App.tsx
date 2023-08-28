import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './components/layout/Home';
import Login from './components/Services/Login';
import SignUp from './components/Services/SignUp';
import CartPage from './components/layout/CartPage';
import BookDetails from './components/layout/BookDetails';
import ProfilePage from './components/layout/ProfilePage';
import PrivateRoute from './components/Services/PrivateRoute';

const App = () => {
  const storedUserProfile = localStorage.getItem("userProfile");
  console.log("storedUserProfile: ", storedUserProfile)
  
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/cart" element={<CartPage />} />
        {/* <Route path="/profile" element={<ProfilePage />} /> */}
        <Route path="/bookDetails/:id" element={<BookDetails />} />
        <Route path="/" element={<PrivateRoute isAuthenticated={!!storedUserProfile}/>}>
          <Route path="/profile" element={<ProfilePage/>}/>
        </Route>
      </Routes>
    </BrowserRouter>
  );  
};

export default App;

