import { Button } from '@mui/material';
import axios from 'axios';
import { useEffect, useRef } from 'react';
import { useLocation } from 'react-router-dom';
import background from '../BackgroundImages/babkground.png'

function BellSkirtCut(){
    const location = useLocation();
    const skirt=location.state.skirt;
    let  arr=[];
    const WaistCircumference = skirt.waistCircumference;
    const SkirtLength=skirt.skirtLength*7 //skirt.SkirtLength;
    const formula=(180-(WaistCircumference*(1/3)));

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
    const canvas = canvasRef.current;
    const ctx = canvas.getContext('2d');

    ctx.fillStyle = '#FFF'; // set the fill color to white
    ctx.fillRect(0, 0, canvas.width, canvas.height);
    // Draw the cloth-בד
    ctx.beginPath();
    ctx.moveTo(10, 10); // Top left corner
    ctx.lineTo(canvas.width, 10); // Top right corner
    ctx.lineTo(canvas.width, canvas.height); // Bottom right corner
    ctx.lineTo(10, canvas.height); // Bottom left corner
    ctx.lineTo(10, 10); // Bottom left corner
    
     //drow Front
     ctx.moveTo(20, 20+formula);
     ctx.lineTo(20,10+formula+SkirtLength);
     ctx.moveTo(20+formula, 20);
     ctx.lineTo(20+formula+SkirtLength,20);

     ctx.moveTo(20, 20+formula)
     ctx.quadraticCurveTo( formula*(4/5),formula*(3/4) , 20+formula, 20)
     ctx.moveTo(20,10+formula+SkirtLength)
     ctx.quadraticCurveTo( formula*2.8,formula*2.8, 20+formula+SkirtLength,20)
     
    ctx.font = '24px Arial';
    ctx.fillStyle = "blue";
    ctx.fillText("x", 35, 20+formula+(SkirtLength/4) );
    ctx.fillText("x", 35, 20+formula+(SkirtLength/4)+40 );
    ctx.fillText("x", 35, 20+formula+(SkirtLength/4)+80);

     //drow Back
     ctx.moveTo(canvas.width-20,10+formula+SkirtLength);
     ctx.lineTo(canvas.width-20,10+formula+SkirtLength+SkirtLength);
     ctx.quadraticCurveTo( canvas.width-20-(formula*5/6),10+formula+SkirtLength+SkirtLength , canvas.width-20-formula, 10+formula+SkirtLength+SkirtLength+formula)
     ctx.lineTo(canvas.width-20-formula-SkirtLength, 10+formula+SkirtLength+SkirtLength+formula);
     ctx.quadraticCurveTo( formula*(4/5),SkirtLength+SkirtLength , canvas.width-20,10+formula+SkirtLength)

     
     // Write Hebrew text on the canvas
     ctx.fillText('front',canvas.width/2, 20+formula );
     ctx.fillText('back',canvas.width*2/3,10+formula+SkirtLength+SkirtLength );
     ctx.fillText('Your Cut:',canvas.width/2-20,canvas.height/3)
     ctx.strokeStyle = '#000';
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
    return (
    <> 
          <div style={{ display: 'flex', justifyContent: 'flex-start' }}>
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-start' }}>
        <Button onClick={() => handleConvertToImageAndPrint('print')} variant="contained" style={{marginTop:"200px",marginLeft:"300px"}}>Print Image</Button>
        <img src={background} alt="Skirt" style={{marginLeft:"170px"}}/>
      </div>
      <div style={{ marginTop: '20px' }}>
        <canvas id='canvas' ref={canvasRef} width={60 * 10} height={150 * 10} style={{marginLeft:"120px"}}/>
      </div>
    </div>
    </>
    );
}
export default BellSkirtCut;