import React from 'react';
import { render, screen } from '@testing-library/react';
import Footer from './Footer';

describe('Footer', () => {
  test('renders the logo', () => {
    render(<Footer />);
    const logo = screen.getByAltText('logo');
    expect(logo).toBeInTheDocument();
  });

  test('renders social media links', () => {
    render(<Footer />);
    
    const facebookLink = screen.getByRole('link', { name: 'Facebook' });
    const twitterLink = screen.getByRole('link', { name: 'Twitter' });
    const instagramLink = screen.getByRole('link', { name: 'Instagram' });
    const linkedinLink = screen.getByRole('link', { name: 'LinkedIn' });

    expect(facebookLink).toBeInTheDocument();
    expect(facebookLink.href).toBe('https://www.facebook.com/');
    expect(twitterLink).toBeInTheDocument();
    expect(twitterLink.href).toBe('https://www.twitter.com/');
    expect(instagramLink).toBeInTheDocument();
    expect(instagramLink.href).toBe('https://www.instagram.com/');
    expect(linkedinLink).toBeInTheDocument();
    expect(linkedinLink.href).toBe('https://www.linkedin.com/');
  });

  test('renders footer contact info', () => {
    render(<Footer />);
    const address = screen.getByText(/2750 Quadra Street Golden Victoria Road, New York, USA/i);
    const phone = screen.getByText(/\+1 \(123\) 456 7890/i);
    const email = screen.getByText(/hello@eLearniv.com/i);

    expect(address).toBeInTheDocument();
    expect(phone).toBeInTheDocument();
    expect(email).toBeInTheDocument();
  });

  test('renders footer navigation links', () => {
    render(<Footer />);
    const homeLink = screen.getByRole('link', { name: /home/i });
    const aboutLink = screen.getByRole('link', { name: /about us/i });
    const coursesLink = screen.getByRole('link', { name: /courses/i });
    const contactLink = screen.getByRole('link', { name: /contact us/i });
    const faqLink = screen.getByRole('link', { name: /faq/i });

    expect(homeLink).toBeInTheDocument();
    expect(aboutLink).toBeInTheDocument();
    expect(coursesLink).toBeInTheDocument();
    expect(contactLink).toBeInTheDocument();
    expect(faqLink).toBeInTheDocument();
  });

  test('renders privacy policy and terms & conditions links', () => {
    render(<Footer />);
    const privacyPolicyLink = screen.getByRole('link', { name: /privacy policy/i });
    const termsConditionsLink = screen.getByRole('link', { name: /terms & conditions/i });

    expect(privacyPolicyLink).toBeInTheDocument();
    expect(termsConditionsLink).toBeInTheDocument();
  });

  test('renders the current year in the footer', () => {
    render(<Footer />);
    const currentYear = new Date().getFullYear();
    const yearText = screen.getByText(`${currentYear} eLearning System`);
    expect(yearText).toBeInTheDocument();
  });
});
