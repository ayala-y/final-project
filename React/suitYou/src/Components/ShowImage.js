import axios from "axios";
import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

const ShowImage = () => {

const [skirtList, setSkirtList] = useState([]);
let currentUser= useSelector(state=>state.user.currentUser)

useEffect(()=>{
  console.log(currentUser);
  axios.get(`https://localhost:7247/api/Skirt/GetAllSkirtsOfUser?user_id=${currentUser.id}`).then((res=>{
    setSkirtList(res.data);
  }))
},[]);

  return (
    <>
     <h1>My Skirts</h1>
      <ul style={{ whiteSpace: 'nowrap' }}>
        {skirtList.map((skirt) => (
          <li key={skirt.Id} style={{ display: 'inline-block', margin: '10px' }}>
            <h2>{skirt.name}</h2>
            <img src={`https://localhost:7247/api/Skirt/${skirt.imgName}`} width={"200px"} height={"200px"}/>
            <h3>{skirt.sectionName}</h3>
            <img src={`https://localhost:7247/api/Skirt/${skirt.skirtCutImgName}`} width={"500px"} height={"500px"}/>
          </li>
        ))}
      </ul>
      
    </>
  );
};

export default ShowImage;
  
