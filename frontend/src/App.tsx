import React, { useState, useEffect } from 'react';
import { fetchData } from './baseApi';
import { useSelector } from 'react-redux';
import { GlobalState } from './redux/store';
import useAppSelector from './hooks/useAppSelector';
import { createUser, fetchAllUsers, updataUserReducer } from './redux/reducers/usersReducer';
import useAppDispatch from './hooks/useAppDispatch';
import Header from './components/layout/Header';

const App = () => {
  const user = useAppSelector(state => state.userReducer)
  console.log(user)
  const dispatch = useAppDispatch()
  
  useEffect(()=> {
    dispatch(fetchAllUsers())
  }, [])

  const addNewUser = () => {
    // dispatch(createUser({}))
    dispatch(fetchAllUsers())
  }

  return (
    <div className="App">
      <h1>Fetched Data from API</h1>
      <Header/>
      <button onClick={addNewUser}> New User</button>
      {/* <ul>
        {data.map((item, index) => (
          <li key={index}>{item.title}</li>
          
        ))}
      </ul> */}
    </div>
  );  
};

export default App;

