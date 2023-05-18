import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import SignIn from './Components/SignIn';
import SignUp from './Components/SignUp';
import DeleteUser from './Components/DeleteUser';
import Home from './Components/Home';
import SkirtSizes from './Components/SkirtSizes';
import StraightSkirtCut from './Components/Canvas/StraightSkirtCut'
import ASkirtCut from './Components/Canvas/ASkirtCut'
import PleatedSkirtCut from './Components/Canvas/PleatedSkirtCut'
import BellSkirtCut from './Components/Canvas/BellSkirtCut'
import NotASkirtCut from './Components/Canvas/NotASkirtCut'
import ShowImage from './Components/ShowImage';
import ShowOneCut from './Components/showOneCut';
import ShowCut from './Components/ShowCut';
function App() {
  return (
    <div className="App">
    <BrowserRouter>
    <Routes>
    <Route index element={<SignIn/>}/>
    <Route path='/signIn' element={<SignIn/>}/> 
    <Route path='/signUp' element={<SignUp/>}/> 
    <Route path='/deleteUser' element={<DeleteUser/>}/> 
    <Route path='/home' element={<Home/>}/> 
    <Route path='/skirtSizes' element={<SkirtSizes/>}/> 
    <Route path='/straightSkirtCut' element={<StraightSkirtCut/>}/>  
    <Route path='/aSkirtCut' element={<ASkirtCut/>}/>     
    <Route path='/pleatedSkirtCut' element={<PleatedSkirtCut/>}/>     
    <Route path='/bellSkirtCut' element={<BellSkirtCut/>}/>     
    <Route path='/notASkirtCut' element={<NotASkirtCut/>}/> 
    <Route path='/showImage' element={<ShowImage/>}/> 
    <Route path='/showOneCut' element={<ShowOneCut/>}/> 
    <Route path='/showCut' element={<ShowCut/>}/> 
    </Routes>
    </BrowserRouter>
    </div>
  );
}

export default App;
