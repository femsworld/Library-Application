import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './components/layout/Home';
import Login from './components/Services/Login';
import SignUp from './components/Services/SignUp';
import CartPage from './components/layout/CartPage';



const App = () => {
  
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/cart" element={<CartPage />} />
      </Routes>
    </BrowserRouter>
  );  
};

export default App;

