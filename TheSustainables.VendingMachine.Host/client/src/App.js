import React, { useState, useEffect } from 'react'
import { Container, Row, Col } from 'reactstrap'
import { DndProvider } from 'react-dnd'
import axios from 'axios'
import HTML5Backend from 'react-dnd-html5-backend'
import Machine from './Components/Machine'
import UserCoins from './Components/UserCoins'
import ReturnedCoins from './Components/ReturnedCoins'
import './App.css'

function App() {
    const defaultMessage = "Insert coin"
    const [credit, setCredit] = useState(0)
    const [returnedCoins, setReturnedCoins] = useState([])
    const [lcdText, setLcdText] = useState(defaultMessage)
    const [productSold, setProductSold] = useState(false);
    const closeReturnedCoins = () => {
        setReturnedCoins([])
    }

    const updateLcdTextWithCredit = (credit) => {
        if (credit > 0) {
            setLcdText(`credit: ${(credit / 100).toFixed(2)}`);
        } else {
            setLcdText(defaultMessage);
        }
    }
    useEffect(() => {

        updateLcdTextWithCredit(credit)
    }, [credit])

    const returnCoins = () => {
        if (credit > 0) {
            axios.delete('api/machine/userCashTray')
                .then((result) => {
                    setReturnedCoins(result.data)
                    setCredit(0);
                })
        }
    }

    const sellProduct = (id) => {
        axios.post('api/machine/products/purchase', { productId: id })
            .then((response) => {
                if (response.data.succeed) {
                    setReturnedCoins(response.data.change)
                    setCredit(0)
                    setLcdText("Thank you")
                    setProductSold(true);
                } else {
                    setLcdText(response.data.error)
                }
            })
    }

    useEffect(() => {
        axios.get('api/machine/userCashTray')
            .then((result) => setCredit(result.data))
    }, [])

    return (
        <div className="App">
            <DndProvider backend={HTML5Backend}>
                <Container>
                    <Row>
                        <Col md={6}>
                            <Machine credit={credit} returnCoins={returnCoins} lcdText={lcdText} sellProduct={sellProduct} productSold={productSold} setProductSold={setProductSold}></Machine>
                        </Col>
                        <Col md={6}>
                            <UserCoins updateCredit={setCredit}></UserCoins>
                            <ReturnedCoins returnedCoins={returnedCoins} close={closeReturnedCoins}></ReturnedCoins>
                        </Col>
                    </Row>
                </Container>
            </DndProvider>
        </div>
    );
}

export default App
