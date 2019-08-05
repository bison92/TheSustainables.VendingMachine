import React from 'react';
import './App.css';
import Machine from './Components/Machine';
import UserCoins from './Components/UserCoins';
import { Container, Row, Col } from 'reactstrap';

function App() {
    return (
        <div className="App">
            <Container>
                <Row>
                    <Col md={8}>
                        <Machine></Machine>
                    </Col>
                    <Col md={4}>
                        <UserCoins></UserCoins>
                    </Col>
                </Row>
            </Container>
        </div>
    );
}

export default App;
