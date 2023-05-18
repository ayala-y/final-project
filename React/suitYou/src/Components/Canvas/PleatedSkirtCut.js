import { useLocation } from 'react-router-dom';
import { useRef } from 'react';
import { useEffect } from 'react';
import { Button } from '@mui/material';
import axios from 'axios';
import background from '../BackgroundImages/babkground.png'

function PleatedSkirtCut(){

    const location = useLocation();
    const skirt=location.state.skirt;

    // const WaistCircumference =(skirt.waistCircumference/2+1)*10; //skirt.WaistCircumference;//היקף מותן
    // const HipCircumference=(skirt.hipCircumference/2+2)/2*10;//(skirt.HipCircumference/2+2)/2;//היקף ירכיים
    const SkirtLength=skirt.skirtLength*10;//skirt.SkirtLength;//אורך חצאית
    // const HeightHip=skirt.heightHip*10;//skirt.HeightHip;//גובה ירכיים

    let  arr=[];

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
     const numPleatedForoneSide=5;
     const amountOfClothToEachPleated=(canvas.width)/numPleatedForoneSide;
     const dipPleated=amountOfClothToEachPleated/3;

     ctx.moveTo(10,10+SkirtLength); 
     ctx.lineTo(canvas.width, 10+SkirtLength);
     
     //drow pleatCloth
     ctx.font = '24px Arial';
     ctx.fillStyle = "blue";
     ctx.fillText("x", 30, 200 );
     ctx.fillText("x", 30, 200 +40 );
     ctx.fillText("x", 30, 200 + 80 );
   
    let isPleated=false;
    let endPleated=10;
     for (let index = 1; index <= numPleatedForoneSide*2; index++) {
        if(isPleated){
            ctx.moveTo(dipPleated+endPleated,10);
            ctx.lineTo(dipPleated+endPleated,10+SkirtLength);
            
            ctx.moveTo(endPleated+dipPleated/2,25);
            ctx.lineTo(endPleated+dipPleated-(dipPleated*1.5),10+(SkirtLength/4));

            ctx.moveTo(endPleated+dipPleated/2,SkirtLength/4);
            ctx.lineTo(endPleated+dipPleated-(dipPleated*1.5),10+(SkirtLength/4*2));
            
            ctx.moveTo(endPleated+dipPleated/2,SkirtLength/4*2);
            ctx.lineTo(endPleated+dipPleated-(dipPleated*1.5),10+(SkirtLength/4*3));
            
            ctx.moveTo(endPleated+dipPleated/2,SkirtLength/4*3);
            ctx.lineTo(endPleated+dipPleated-(dipPleated*1.5),10+(SkirtLength)-15);

            endPleated=dipPleated+endPleated;
            isPleated=false;

        }
        else{
            ctx.moveTo(dipPleated+endPleated,10);
            ctx.lineTo(dipPleated+endPleated,10+SkirtLength);
            endPleated=endPleated+dipPleated*2;
            isPleated=true;
        }
    }

    ctx.fillText('front', canvas.width/2+20, 200 );
    ctx.fillText('back', canvas.width/2+20,10+SkirtLength+20+200)

    //drow Back
     ctx.moveTo(10,10+SkirtLength+20); 
     ctx.lineTo(canvas.width, 10+SkirtLength+20);

     ctx.moveTo(10,10+SkirtLength+20+SkirtLength); 
     ctx.lineTo(canvas.width, 10+SkirtLength+20+SkirtLength);
     
     //drow pleatCloth
     ctx.fillText("x", 30, 10+SkirtLength+20+200 );
     ctx.fillText("x", 30, 10+SkirtLength+20+200 +40 );
     ctx.fillText("x", 30, 10+SkirtLength+20+200 + 80 );
    
     let isPleated1=false;
     let endPleated1=10;
        for (let index = 1; index <= numPleatedForoneSide*2; index++) {
            if(isPleated1){
                ctx.moveTo(dipPleated+endPleated1,10+SkirtLength+20);
                ctx.lineTo(dipPleated+endPleated1,10+SkirtLength+20+SkirtLength);
             
                ctx.moveTo(endPleated1+dipPleated/2,10+SkirtLength+20+15);
                ctx.lineTo(endPleated1+dipPleated-(dipPleated*1.5),10+SkirtLength+20+(SkirtLength/4));
 
                ctx.moveTo(endPleated1+dipPleated/2,10+SkirtLength+20+SkirtLength/4);
                ctx.lineTo(endPleated1+dipPleated-(dipPleated*1.5),10+SkirtLength+20+(SkirtLength/4*2));
             
                ctx.moveTo(endPleated1+dipPleated/2,10+SkirtLength+20+SkirtLength/4*2);
                ctx.lineTo(endPleated1+dipPleated-(dipPleated*1.5),10+SkirtLength+20+(SkirtLength/4*3));
             
                ctx.moveTo(endPleated1+dipPleated/2,10+SkirtLength+20+SkirtLength/4*3);
                ctx.lineTo(endPleated1+dipPleated-(dipPleated*1.5),10+SkirtLength+20+(SkirtLength)-15);
 
                endPleated1=dipPleated+endPleated1;
                isPleated1=false;
            }
            else{
                ctx.moveTo(dipPleated+endPleated1,10+SkirtLength+20);
                ctx.lineTo(dipPleated+endPleated1,10+SkirtLength+20+SkirtLength);
                endPleated1=endPleated1+dipPleated*2;
                isPleated1=true;
            }
        }
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
export default PleatedSkirtCut;