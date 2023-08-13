import useAppSelector from './hooks/useAppSelector';
import useAppDispatch from './hooks/useAppDispatch';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

import { Home } from '@mui/icons-material';

const App = () => {
  const user = useAppSelector(state => state.userReducer)
  console.log(user)
  const dispatch = useAppDispatch()
  
  // useEffect(()=> {
  //   dispatch(fetchAllUsers())
  // }, [])

  // const addNewUser = () => {
  //   // dispatch(createUser({}))
  //   dispatch(fetchAllUsers())
  // }

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
      </Routes>
    </BrowserRouter>
  );  
};

export default App;

