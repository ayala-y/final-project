import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function Copyright(props) {
    return (
      <Typography variant="body2" color="text.secondary" align="center" {...props}>
        {'Copyright © '}
        <Link color="inherit" href="https://mui.com/">
          Your Website
        </Link>{' '}
        {new Date().getFullYear()}
        {'.'}
      </Typography>
    );
  }

const theme = createTheme();

function DeleteUser(){

    const navigate = useNavigate();

    const handleSubmit = (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        console.log({
          name:data.get('name'),
          email: data.get('email'),
          password: data.get('password'),
        });
        let user={
            "Id":0,
            "Name":data.get('name'),
            "Password":data.get('password'),
            "Email":data.get('email'),
        }
        if(user.Email!=''&&user.Password!=''){
            axios.get(`https://localhost:7247/api/User/${user.Email}/${user.Password}`).then((response)=>{
                if(response.status!=200){
                    alert("משתמש לא קיים במאגר הלקוחות")
                    navigate('/signIn');
                }
                else{
                  deleteUser(response.data);
                }
            })
        }
        else{
            alert("יש למלא את כל השדות");
        }
      };

    const deleteUser=(user)=>{
        if(user.id!=undefined){
          deleteAllSkirtsOfUser(user.id);
            axios.delete(`https://localhost:7247/api/User/id?id=${user.id}`).then((response)=>{
                if(response.status!=200){
                    alert("משתמש לא נמחק");
                    navigate('/signIn');
                }
                alert("משתמש נמחק בהצלחה");
                navigate('/signIn');
            })
        }
        return;
    }

    const deleteAllSkirtsOfUser=(id)=>{
      axios.delete(`https://localhost:7247/api/Skirt/DeleteAllSkirtsOfUser?id=${id}`).then((response)=>{
        if(response.status!=200){
          alert("חצאיות המשתמש לא נמחקו בהצלחה")
          return;
        }
        alert("חצאיות המשתמש נמחקו בהצלחה")
        return;
      })
    }

    return(
        <> <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            {/* <LockOutlinedIcon /> */}
          </Avatar>
          <Typography component="h1" variant="h5">
            Delete User
          </Typography>
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <TextField
                  autoComplete="given-name"
                  name="name"
                  required
                  fullWidth
                  id="name"
                  label="name"
                  autoFocus
                />
              </Grid>

              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="email"
                  label="Email Address"
                  name="email"
                  autoComplete="email"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  autoComplete="new-password"
                />
              </Grid>
              <Grid item xs={12}>
              </Grid>
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Delete User
            </Button>
            <Grid container justifyContent="flex-end">
              <Grid item>
              </Grid>
            </Grid>
          </Box>
        </Box>
        <Copyright sx={{ mt: 5 }} />
      </Container>
    </ThemeProvider>
        </>
    );
}
export default DeleteUser