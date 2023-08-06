import React, { useState, useEffect } from 'react';
import { fetchData } from './baseApi';

const App = () => {
  const [data, setData] = useState<any[]>([]);

  useEffect(() => {
    fetchData()
      .then((fetchedData) => {
        if (Array.isArray(fetchedData)) {
          setData(fetchedData);
        } else {
          console.error('Data is not an array:', fetchedData);
        }
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
          <li key={index}>{item.title}</li>
        ))}
      </ul>
    </div>
  );
};

export default App;

