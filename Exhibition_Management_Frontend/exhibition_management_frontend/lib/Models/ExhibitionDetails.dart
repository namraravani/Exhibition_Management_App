class ExhibitionDetails {
  final String bannerImageUrl;
  final List<String> venuePhotos;
  final String venueName;
  final String address;
  final String timeDuration;
  final int numberOfTables;
  final String description;

  ExhibitionDetails({
    required this.bannerImageUrl,
    required this.venuePhotos,
    required this.venueName,
    required this.address,
    required this.timeDuration,
    required this.numberOfTables,
    required this.description,
  });
}
