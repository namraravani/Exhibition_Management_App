import 'package:flutter/material.dart';
import 'package:exhibition_management_frontend/Tools/ColorPalette.dart'; // Import the color palette
import 'package:exhibition_management_frontend/View/HomePage/HomePage.dart'; // Adjust the path as needed

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Exhibition Management',
      home: HomePage(),
    );
  }
}
