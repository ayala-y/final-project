import { useLocation } from "react-router-dom";

function ShowCut(){

    const location = useLocation();
    const skirt=location.state.response;

    return(
        <>
        <h1>My Skirt</h1>
        <h2></h2>
        <img src={`https://localhost:7247/api/Skirt/${skirt.imgName}`} width={"200px"} height={"200px"}/>
        <img src={`https://localhost:7247/api/Skirt/${skirt.skirtCutImgName}`} width={"500px"} height={"500px"}/>
        </>
    )
}export default ShowCut