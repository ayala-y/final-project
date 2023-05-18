import { Button, TextField } from "@mui/material";
import axios from "axios";
import React, {  useRef } from "react";
import background from './BackgroundImages/babkground.png'
import { useNavigate } from "react-router-dom";

function ShowOneCut(){

    const Imgname=useRef(null);

    const navigate=useNavigate();
    const showImage = () => {
      let name=Imgname.current.value;
      axios.get(`https://localhost:7247/api/Skirt/GetSkirtByName?Name=${name}`).then((res)=>{
      return res.data;
      }).then((response=>{
        navigate('/showCut',{ state: { response } });
    }))
    }

    return(
    <>
    <form>
      <TextField
        id="ImgName"
        label="ImgName"
        variant="outlined"
        inputRef={Imgname}
        required
        style={{marginRight: '100px'}}
      />
      <Button variant="contained" onClick={showImage} >Show Image</Button>
    </form>
      <img src={background} alt="Skirt"/>
    </>

    );
}export default ShowOneCut