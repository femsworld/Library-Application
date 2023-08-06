// import axios from 'axios';
// import React from 'react'

// const App = () => {
  
//   return (
//     <div>App</div>
//   )
// }

// export default App

import React, { useState, useEffect } from 'react';
import { fetchData } from './baseApi';

function App() {
  const [data, setData] = useState<any[]>([]); // Adjust the type as per your API response structure

  useEffect(() => {
    fetchData()
      .then((fetchedData) => {
        setData(fetchedData);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
  }, []);

  return (
    <div className="App">
      <h1>Fetched Data from API</h1>
      <ul>
        {data.map((item, index) => (
          <li key={index}>{item.name}</li> // Adjust the property according to your API response
        ))}
      </ul>
    </div>
  );
}

export default App;
