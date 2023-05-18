import { useLocation, useNavigate } from "react-router-dom";
import { useRef, useState } from "react";
import axios from "axios";
import { useSelector } from "react-redux";
import { Button, TextField } from "@mui/material";
import background from './BackgroundImages/babkground.png';


function SkirtSizes() {
    const location = useLocation();
    const file = location.state;

    let currentUser= useSelector(state=>state.user.currentUser)

    const navigate = useNavigate();

    const waistCircumference = useRef(null);
    const hipCircumference = useRef(null);
    const skirtLength = useRef(null);
    const heightHip = useRef(null);

    const whichskirt = async (skirt) => {
        console.log(skirt);
        console.log(skirt.imgName);
        let Name = skirt.imgName;
        let response = await axios.get(`https://localhost:7247/api/Skirt/GetTypeOfSkirtByName?Name=${Name}`);
        if (response.status !== 200) {
            console.log(response);
            return;
        }
        if (response.data === "a streight skirt") {
            navigate("/straightSkirtCut", { state: { skirt } })
        }
        else {
            if (response.data === "a A skirt") {
                navigate("/aSkirtCut", { state: { skirt } })
            }
            else {
                if (response.data === "a pleated skirt") {
                    navigate("/pleatedSkirtCut", { state: { skirt } })
                }
                else {
                    if (response.data === "a bell skirt") {
                        navigate("/bellSkirtCut", { state: { skirt } })
                    }
                    else {
                        navigate("/notASkirtCut");
                    }
                }
            }
        }

    }

    const handleSubmit = (e) => {
        e.preventDefault()
        console.log(currentUser);
        console.log(file);
        const skirtFileAndSizes = {
            waistCircumference: waistCircumference.current.value,
            hipCircumference: hipCircumference.current.value,
            skirtLength: skirtLength.current.value,
            heightHip: heightHip.current.value,
            files: file
        }
        const formData = new FormData();
        formData.append("Id", 0)
        formData.append("ImgName", '')
        formData.append("WaistCircumference", +skirtFileAndSizes.waistCircumference)
        formData.append("HipCircumference", +skirtFileAndSizes.hipCircumference)
        formData.append("SkirtLength", +skirtFileAndSizes.skirtLength)
        formData.append("HeightHip", +skirtFileAndSizes.heightHip)
        formData.append("SkirtCutImgName", '')
        formData.append("UserId", currentUser.id)
        formData.append("files", skirtFileAndSizes.files.file)
        axios.post("https://localhost:7247/api/Skirt/Post", formData)
            .then(response => {
                console.log(response.data.imgName);
                console.log(response.data);
                whichskirt(response.data);
            })
            .catch(error => { console.error(error) })
    }

    return (<>
    <p style={{font:"initial",color:"lightblue",fontSize:"50px"}}>please enter your sizes</p>
     <div style={{ display: "flex", alignItems: "center", justifyContent: "center", height: "100vh" }}>
        <form onSubmit={handleSubmit} style={{marginRight: '100px',marginBottom:'200px'}}>
        <div style={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "space-between" }}>

        <TextField
          id="waist-circumference"
          label="Waist Circumference"
          variant="outlined"
          inputRef={waistCircumference}
          required
          style={{ marginBottom: "1rem" }}
        />
        <TextField
          id="hip-circumference"
          label="Hip Circumference"
          variant="outlined"
          inputRef={hipCircumference}
          required
          style={{ marginBottom: "1rem" }}
        />
        <TextField
          id="skirt-length"
          label="Skirt Length"
          variant="outlined"
          inputRef={skirtLength}
          required
          style={{ marginBottom: "1rem" }}
        />
        <TextField
          id="height-hip"
          label="Height Hip"
          variant="outlined"
          inputRef={heightHip}
          require            
          style={{ marginBottom: "1rem" }}
        />
        <Button variant="contained" type="submit">
          To Send
        </Button>
        </div>
      </form>

      <img src={background} alt="Skirt" />

      </div>    
      </>)
}
export default SkirtSizes

