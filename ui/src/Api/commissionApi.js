

const API_BASE = process.env.REACT_APP_API_URL;

 export const GetCommission = async (salesData) => {
  console.log(API_BASE);
  
  try {
    const response = await fetch(`${API_BASE}/commision`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(salesData)
    });

    if (!response.ok) {
      throw new Error('Network response was not ok');
    }

    const data = await response.json();
    return data;
  } 
  catch (error) 
  {
       throw error;
  }



}