using System;
using System.Collections.Generic;

public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _products = new List<Product>();
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetTotalCost()
    {
        double totalProductCost = 0;
        
        foreach (Product product in _products)
        {
            totalProductCost += product.GetTotalCost();
        }

        // Add shipping cost
        double shippingCost = _customer.LivesInUSA() ? 5.00 : 35.00;
        
        return totalProductCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "PACKING LABEL\n";
        label += "==============\n";
        
        foreach (Product product in _products)
        {
            label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
        }
        
        return label;
    }

    public string GetShippingLabel()
    {
        string label = "SHIPPING LABEL\n";
        label += "===============\n";
        label += _customer.GetName() + "\n";
        label += _customer.GetAddress().GetFullAddress();
        return label;
    }

    public List<Product> GetProducts()
    {
        return _products;
    }

    public Customer GetCustomer()
    {
        return _customer;
    }
}
