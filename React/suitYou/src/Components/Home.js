import { Button } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom"
import { useSelector } from "react-redux";
import image from './BackgroundImages/image.png'



function Home(){

    const [file, setFile] = useState('');
    const [fileName, setFileName] = useState('');
    const [showImages, setShowImages] = useState(false);
    const [buttonClicked, setButtonClicked] = useState(false);

    let currentUser= useSelector(state=>state.user.currentUser)
    const navigate=useNavigate();

  const saveFile = (e) => {
    console.log(e.target.files[0]);
    setFile(e.target.files[0])
    setFileName(e.target.files[0].name)
    console.log(file);
    navigate('/skirtSizes', { state:{file:e.target.files[0]}})
  }

  const showMyCutToSpecificImage=()=>{
    navigate('/showOneCut')
  }

  const showAllMySkirts=()=>{
    navigate('/showImage');
  }

    return(
      <>
      <input type="file" id="image-upload" onChange={saveFile} style={{ display: "none" }} />
      <label htmlFor="image-upload">
        <div className={showImages ? 'hidden' : 'flex'}>
        <div className="background-image" style={{ backgroundImage: `url(${image})`, height: "100vh", width: "100vw", display: buttonClicked ? 'none' : 'block' }}>     
        <Button variant="contained" component="span" style={{ marginTop: '16px', marginRight: '16px' }}> Create cut To Image </Button>
          <Button variant="contained" onClick={showAllMySkirts} style={{ marginTop: '16px' , marginRight: '16px' }}>show All My Skirts</Button>
          <Button variant="contained" onClick={() => { showMyCutToSpecificImage(); }} style={{ marginTop: '16px' }}>show My Cut To Specific Image</Button>
          </div>
        </div>
      </label>
        </>
    )
}export default Home