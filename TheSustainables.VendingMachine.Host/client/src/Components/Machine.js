import React, { useState, useEffect } from 'react'
import { Container, Row, Col } from 'reactstrap'
import axios from 'axios'
import Product from '../Components/Product/Product'
import SlotTarget from './DragAndDrop/SlotTarget'
import './Machine.css'

const Machine = (props) => {

    const [products, setProducts] = useState([])

    useEffect(() => {
        axios.get('api/machine/products')
            .then((response) => {
                setProducts(response.data.products)
            })
    }, [])

    return (
        <Container className="machine">
            <Row>
                <Col lg={{ size: 5, order: 2 }}>
                    <div className="panel">
                        <div className="lcdText">{props.lcdText}</div>
                        <SlotTarget className="insertCoin"></SlotTarget>
                        <button className="returnButton" onClick={props.returnCoins}>Return cash</button>
                    </div>
                </Col>
                <Col lg={{ size: 7, order: 1 }}>
                    <div className="showcase">
                        {products.map((product, i) =>
                            <Product key={i} name={product.name} price={product.price} onClick={() => { props.sellProduct(product.id); }} />)}
                    </div>
                </Col>
            </Row>
            <Row>
                <Col lg={7}>
                    <div className={props.productSold ? "tray tray-product" : "tray"} onClick={() => props.setProductSold(false)}>
                        <div className="tray-cover">
                        </div>
                    </div>
                </Col>
            </Row>
        </Container>
    )
}

export default Machine;