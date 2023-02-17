// Calculator.cpp : Implementation of CCalculator

#include "pch.h"
#include "Calculator.h"

// CCalculator

STDMETHODIMP CCalculator::Add(LONG x, LONG y, LONG* presult)
{
	*presult = x + y;

    return S_OK;
}

STDMETHODIMP CCalculator::Subtract(LONG x, LONG y, LONG* presult)
{
	*presult = x - y;

    return S_OK;
}
