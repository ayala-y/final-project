import React from 'react';
import background from '../BackgroundImages/babkground.png'
import { Button } from '@mui/material';
import {useNavigate} from 'react-router-dom';

function NotASkirtCut(){
  const navigate=useNavigate();
  return (
    <>
    <div
      style={{
        backgroundColor: '#f8f8f8',
        border: '1px solid #ccc',
        borderRadius: '5px',
        padding: '10px',
        textAlign: 'center',
        fontSize: '1.2rem',
        fontWeight: 'bold',
        color: 'blue',
        boxShadow: '2px 2px 5px rgba(0, 0, 0, 0.3)',
      }}
    >
      <p>the image isnt image of skirt!!</p>
    </div> 
    <img src={background} alt="Skirt" style={{marginTop:"210px"}}/> 
    <Button onClick={()=>navigate('/home')} variant='contained' style={{marginTop:"2px"}}>To Home</Button>

    </>
  );
};

export default NotASkirtCut;

