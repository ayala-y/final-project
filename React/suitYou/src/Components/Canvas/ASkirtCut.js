import { Button } from '@mui/material';
import axios from 'axios';
import { useRef } from 'react';
import { useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import background from '../BackgroundImages/babkground.png'

function ASkirtCut(){

    const location = useLocation();
    const skirt=location.state.skirt;
    let  arr=[];

    const WaistCircumference =(skirt.waistCircumference/2+1)/2*10; //skirt.WaistCircumference;//היקף מותן
    const HipCircumference=(skirt.hipCircumference/2+2)/2*10;//(skirt.HipCircumference/2+2)/2;//היקף ירכיים
    const SkirtLength=skirt.skirtLength*10;//skirt.SkirtLength;//אורך חצאית
    const HeightHip=skirt.heightHip*10;//skirt.HeightHip;//גובה ירכיים


    const canvasRef = useRef();
    useEffect(async () => {
      let promise = new Promise(function (resolve, reject) {
        resolve();
        reject();
      })
      promise.then(createCanvas()).then(() => {
       const img = handleConvertToImageAndPrint(null);
       arr.push(""+skirt.id)
       arr.push(""+skirt.userId)
       arr.push(img.src)
       arr.push(skirt.imgName)
       console.log(arr)
       sendToDB()
      });
      promise.catch(function () { });
  
    }, []);

    const sendToDB = async ()=> {
      let response = await axios.post("https://localhost:7247/api/Skirt",arr);
      if (response.status !== 200) {
        console.log(response);
        return;
      }
      else {
        console.log('your cut was saved successfully!!!!!!');
      }
    }

    const createCanvas = () => {
    // Get the canvas element
    const canvas = canvasRef.current;
    const ctx = canvas.getContext('2d');



    // Draw the cloth-בד
    ctx.beginPath();
    ctx.moveTo(10, 10); // Top left corner
    ctx.lineTo(canvas.width, 10); // Top right corner
    ctx.lineTo(canvas.width, canvas.height); // Bottom right corner
    ctx.lineTo(10, canvas.height); // Bottom left corner
    ctx.lineTo(10, 10); // Bottom left corner

    //drow Frontskirt
    ctx.moveTo(20,200);
    ctx.lineTo(20,200+SkirtLength);
    ctx.moveTo(20,200);
    ctx.quadraticCurveTo(WaistCircumference-10, 200, WaistCircumference, 180)
    ctx.quadraticCurveTo(HipCircumference , 200, HipCircumference+(HipCircumference/3), SkirtLength+150)
    ctx.quadraticCurveTo(WaistCircumference-10 , 200+SkirtLength, 20,200+SkirtLength)
    
    // //drow pleatCloth

    ctx.font = '24px Arial';
    ctx.fillStyle = "blue";
    ctx.fillText("x", 30, 200 + HeightHip );
    ctx.fillText("x", 30, 200 + HeightHip + 40 );
    ctx.fillText("x", 30, 200 + HeightHip + 80 );

    //drow Backskirt
    ctx.moveTo(canvas.width - 20, 200)
    ctx.lineTo(canvas.width - 20, 200 + SkirtLength);
    ctx.moveTo(canvas.width - 20, 200)
    ctx.quadraticCurveTo(canvas.width - WaistCircumference+10, 200, canvas.width - WaistCircumference, 180)
    ctx.quadraticCurveTo(canvas.width-HipCircumference , 200,canvas.width-(HipCircumference+(HipCircumference/3)), SkirtLength+150)
    ctx.quadraticCurveTo(canvas.width-(WaistCircumference-10) , 200+SkirtLength,canvas.width - 20, 200 + SkirtLength)

    ctx.fillText('Your Cut:',canvas.width/2-20,80)
    ctx.fillText('front', 20 + HipCircumference / 4 * 1.5, 200 + SkirtLength / 4);
    ctx.fillText('back', (canvas.width - 20) - (HipCircumference / 2), 200 + SkirtLength / 4);

    ctx.stroke();
    }


const handleConvertToImageAndPrint = (x) => {
  const canvas = canvasRef.current;
  const image = new Image();
  image.src = canvas.toDataURL();
  if (x === 'print') {
    handlePrintImage(image);
  }
  else {
    return image;
  }
}

const handlePrintImage = (image) => {
  const printWindow = window.open('', 'Print', 'height=600,width=800');
  printWindow.document.write('<html><head><title>Print</title></head><body>');
  printWindow.document.write('<img src="' + image.src + '" />');
  printWindow.document.write('</body></html>');
  printWindow.print();
}
const navigate=useNavigate();

const ToHome=()=>{
  navigate('/home')
}
return (
    <>
       <div style={{ display: 'flex', justifyContent: 'flex-start' }}>
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start' }}>

        <Button onClick={() => handleConvertToImageAndPrint('print')} variant="contained" style={{marginTop:"200px",marginLeft:"300px"}}>Print Image</Button>
        <Button onClick={ToHome} variant='contained'>To Home</Button>
        <img src={background} alt="Skirt" style={{marginLeft:"170px"}}/>
      </div>
      <div style={{ marginTop: '20px' }}>
        <canvas id='canvas' ref={canvasRef} width={60 * 10} height={150 * 10} style={{marginLeft:"120px"}}/>
      </div>
    </div>
    </>
    );
}
export default ASkirtCut;