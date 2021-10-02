
import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelope, faUnlockAlt } from "@fortawesome/free-solid-svg-icons";
import { faFacebookF, faGithub, faTwitter } from "@fortawesome/free-brands-svg-icons";
import { Col, Row, Form, Card, Button, FormCheck, Container, InputGroup } from '@themesberg/react-bootstrap';
import { useHistory  } from 'react-router-dom';
import { Routes } from "../../routes";
import BgImage from "../../assets/img/illustrations/signin.svg";
import { domain } from "../../assets/domain";
import {ToastContainer,toast,Zoom} from "react-toastify"
import "react-toastify/dist/ReactToastify.css"
import axios from "axios";
var admin = []
axios.get(domain + "admin")
  .then(function ({data}) {
    // handle success
    admin = data.result;
    console.log(admin)
  })
  .catch(function (error) {
    // handle error
    console.log(error);
  })
  .then(function () {
    // always executed
  });

  const handleFailedToast = ()=>{
    toast.error('🦄Kullanıcı Adı/Şifre Yanlış', {
      position: "top-center",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      });
  }
export default () => {
  var history = useHistory()
  const [userName,setuserName] = React.useState()
  const [password,setpassword] = React.useState()
  const loginhandle = (userName , password)=>{
    if (admin.userName === userName && admin.password === password) {
     history.push(Routes.DashboardOverview.path)
    }
    else{
      handleFailedToast()
    }
    
  }
 
  
  return (
    <main>
      <section className="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
        <Container>
        <ToastContainer/>
          <Row className="justify-content-center form-bg-image" style={{ backgroundImage: `url(${BgImage})` }}>
            <Col xs={12} className="d-flex align-items-center justify-content-center">
              <div className="bg-white shadow-soft border rounded border-light p-4 p-lg-5 w-100 fmxw-500">
                <div className="text-center text-md-center mb-4 mt-md-0">
                  <h3 className="mb-0">PROGRAM GİRİŞ</h3>
                </div>
                <Form className="mt-4" >
                  <Form.Group id="userName" className="mb-4">
                    <Form.Label>Kullanıcı Adı</Form.Label>
                    <InputGroup>
                      <InputGroup.Text >
                        <FontAwesomeIcon icon={faEnvelope} />
                      </InputGroup.Text>
                      <Form.Control autoFocus required type="text" placeholder="Kullanıcı Adı..." onChange={e=>{setuserName(e.target.value);}}/>
                    </InputGroup>
                  </Form.Group>
                  <Form.Group>
                    <Form.Group id="password" className="mb-4">
                      <Form.Label>Şifreniz</Form.Label>
                      <InputGroup>
                        <InputGroup.Text >
                          <FontAwesomeIcon icon={faUnlockAlt} />
                        </InputGroup.Text>
                        <Form.Control required type="password" placeholder="Şİfre..." onChange={r=>setpassword(r.target.value)}/>
                      </InputGroup>
                    </Form.Group>
                   
                  </Form.Group>
                  <Button variant="primary" type="submit" className="w-100" onClick={(e)=>{e.preventDefault();loginhandle(userName,password) }}>
                    Giriş
                  </Button>
                </Form>
              </div>
            </Col>
          </Row>
        </Container>
      </section>
    </main>
  );
};
